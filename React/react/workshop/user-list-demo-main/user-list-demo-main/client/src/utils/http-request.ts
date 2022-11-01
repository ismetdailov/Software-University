const baseURL = process.env.REACT_APP_API_URL;

interface IOptions {
  method: string;
  body?: any;
  headers?: string[][] | Record<string, string> | Headers | undefined;
  credentials?: 'omit' | 'same-origin' | 'include';
}

function httpRequest<R>(method: string, url: string, body?: any): Promise<R> {
  let options: IOptions = { method, credentials: 'include' };

  if (url.includes('upload')) {
    options.body = body;
  } else {
    options.headers = {};
    options.headers['Content-type'] = 'application/json';
    options.body = body ? JSON.stringify(body) : null;
  }

  return fetch(`${baseURL}/${url}`, options)
    .then((res) => Promise.all([res.json(), res.ok]))
    .then(([res, isOk]) =>
      !isOk
        ? (function () {
            throw new Error(res.message);
          })()
        : res
    );
}

export const http = {
  get<R>(url: string) {
    return httpRequest<R>('GET', url);
  },
  post<R>(url: string, body: any) {
    return httpRequest<R>('POST', url, body);
  },
  put<R>(url: string, body: any) {
    return httpRequest<R>('PUT', url, body);
  },
  patch<R>(url: string, body: any) {
    return httpRequest<R>('PATCH', url, body);
  },
  del<R>(url: string) {
    return httpRequest<R>('DELETE', url);
  },
};

