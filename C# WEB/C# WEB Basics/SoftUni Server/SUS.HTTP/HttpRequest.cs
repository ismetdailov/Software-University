﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class HttpRequest
    {
        public static IDictionary<string, Dictionary<string, string>>
            Sessions = new Dictionary<string, Dictionary<string, string>>();
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
            this.FormData = new Dictionary<string, string>();
           
            var lines = requestString.Split(new string[] { HttpConstans.NewLine }, StringSplitOptions.None);
            var headerLine = lines[0];
            var headerLineParts = headerLine.Split(' ');
            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerLineParts[0], true);
            this.Path = headerLineParts[1];
            int lineIndex = 1;
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();
            while (lineIndex < lines.Length)
            {
                var line = lines[lineIndex];
                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    lineIndex++;
                    continue;
                }
                if (isInHeaders)
                {
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    bodyBuilder.AppendLine(line);
                }

                lineIndex++;
            }
            if (this.Headers.Any(x => x.Name == HttpConstans.RequestCookieHeader))
            {
                var cookiesAsString = this.Headers.FirstOrDefault(x =>
                x.Name == HttpConstans.RequestCookieHeader).Value;
                var cookies = cookiesAsString.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var cookieASString in cookies)
                {
                    this.Cookies.Add(new Cookie(cookieASString));
                }
            }
            var sessionCookie = this.Cookies.FirstOrDefault(x => x.Name == HttpConstans.SessionCookieName);
            if (sessionCookie == null)
            {

                var sessionId = Guid.NewGuid().ToString();
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionId, this.Session);
                this.Cookies.Add(new Cookie(HttpConstans.SessionCookieName, sessionId));
            }
            else if (!Sessions.ContainsKey(sessionCookie.Value))
            {

                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionCookie.Value, this.Session);
            }
            else
            {
                this.Session = Sessions[sessionCookie.Value];
            }
            this.Body = bodyBuilder.ToString();
            var parameters = this.Body.Split(new char[] { '&' },StringSplitOptions.RemoveEmptyEntries);
            foreach (var parametur in parameters)
            {
                var parameterParts = parametur.Split('=');
                var name = parameterParts[0];
                var value = WebUtility.UrlDecode(parameterParts[1]);
                if (!this.FormData.ContainsKey(name))
                {
                    this.FormData.Add(name, value);
                }
            }
        }
        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public ICollection<Header> Headers { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public IDictionary<string, string> FormData { get; set; }
        public Dictionary<string,string> Session { get; set; }
        public string Body { get; set; }
    }
}
