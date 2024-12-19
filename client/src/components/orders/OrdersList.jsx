import { useEffect, useState } from 'react';
import { getAllOrders } from '../../managers/orderManager';

export const OrdersList = () => {
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    getAllOrders().then((data) => setOrders(data));
  }, []);

  const formatDateTime = (dateTime) => {
    const date = new Date(dateTime);
    return `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`;
  };

  const formatToDollar = (amount) => {
    const formattedAmount = new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
    }).format(amount);

    return formattedAmount;
  };

  return (
    <div>
      <h1>Orders List</h1>
      <div className="orders-wrapper">
        {orders.map((order) => {
          return (
            <div key={order.id}>
              <h3>Order {order.id}</h3>
              <p>Order Placed on: {formatDateTime(order.orderPlacedOn)}</p>
              <p>Total:{formatToDollar(order.totalPrice)}</p>
            </div>
          );
        })}
      </div>
    </div>
  );
};