import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Layout from './components/core/Layout';
import UserList from './components/users/UsersList';

function App() {
  return (
    <Layout>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<UserList />}></Route>
        </Routes>
      </BrowserRouter>
    </Layout>
  );
}

export default App;
