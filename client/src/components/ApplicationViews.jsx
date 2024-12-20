import { Route, Routes } from 'react-router-dom';
import { AuthorizedRoute } from './auth/AuthorizedRoute';
import Login from './auth/Login';
import Register from './auth/Register';
import { Home } from './home/Home';
import { OrdersList } from './orders/OrdersList';
import { OrderDetails } from './orders/OrderDetails';
import { CreateNewOrderForm } from './orders/CreateNewOrderForm';

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <Home loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />

        <Route path="orders">
          <Route
            index
            element={
              <AuthorizedRoute loggedInUser={loggedInUser}>
                <OrdersList />
              </AuthorizedRoute>
            }
          />
          <Route
            path=":orderId"
            element={
              <AuthorizedRoute loggedInUser={loggedInUser}>
                <OrderDetails />
              </AuthorizedRoute>
            }
          />
          <Route
            path="new"
            element={
              <AuthorizedRoute loggedInUser={loggedInUser}>
                <CreateNewOrderForm loggedInUser={loggedInUser} />
              </AuthorizedRoute>
            }
          />
        </Route>

        <Route
          path="login"
          element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
          path="register"
          element={<Register setLoggedInUser={setLoggedInUser} />}
        />
      </Route>
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
