# User List Demo Application

### MERN stack application - MongoDB; Express; React; Node;

<img src="https://miro.medium.com/proxy/0*hU4zJiyVwWcM0L-w.png" width="400px;" height="150px;">
</br>
</br>

### Live demo of the website can be accessed thanks to heroku - [click here](https://user-list-demo-react.herokuapp.com/?page=1&limit=5).

</br>

## Getting Started

Clone this repository

```
> git clone https://github.com/MihailValkov/user-list-demo.git
```
### Navigate to the server folder and create a '.env' file in the main directory then populate the following information:

- `DB_NAME` -- Mongo Database name;
- `DB_CONNECTION` -- Mongo Database connection string;

### Example

```
DB_NAME=user-list
DB_CONNECTION=mongodb://localhost:27017
```

To start the application, you must run the following command in your terminal:

```
> npm start
```
This command will install all dependencies for the server and client and run the application on port 3000.


## Documentation
- [Client](https://github.com/MihailValkov/user-list-demo/blob/main/client/README.md)
- [Server](https://github.com/MihailValkov/user-list-demo/blob/main/server/README.md)