import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getOrderById } from '../../managers/orderManager';
import './OrderDetails.css';

export const OrderDetails = () => {
  const [order, setOrder] = useState({});
  const { orderId } = useParams();

  useEffect(() => {
    getOrderById(orderId).then((data) => setOrder(data));
  }, [orderId]);

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
      <h2>Order Details</h2>
      <div>
        <h3>Order #{order.id}</h3>
        <p>Order Placed On: {formatDateTime(order.orderPlacedOn)}</p>
        <p>
          Order Taken by{' '}
          {`${order.orderTaker?.firstName} ${order.orderTaker?.lastName}`}
        </p>
        {order.driver && <p>Delivery Driver: {order.driver.firstName}</p>}
        <p>Tip: {order.tip ? formatToDollar(order.tip) : 'No Tip'}</p>

        <div>
          <h4>Pizzas</h4>
          {order.pizzas?.map((pizza) => {
            return (
              <div key={pizza.id} className="pizza-wrapper">
                <div>
                  <h5>Size</h5>
                  <p>{pizza.size}</p>
                </div>
                <div>
                  <h5>Cheese</h5>
                  <p>{pizza.cheese.name}</p>
                </div>
                <div>
                  <h5>Sauce</h5>
                  <p>{pizza.sauce.name}</p>
                </div>

                <div>
                  <h5>Toppings</h5>
                  <ul>
                    {pizza.toppings.map((topping) => (
                      <li key={topping.id}>{topping.name}(.50 ea)</li>
                    ))}
                  </ul>
                </div>

                <div>
                  <h5>Pizza Total</h5>
                  <p>{formatToDollar(pizza.totalWithToppings)}</p>
                </div>
              </div>
            );
          })}
        </div>
      </div>
      <div className="order-total-wrapper">
        <h3>Order Total</h3>
        <p> {formatToDollar(order.totalPrice)}</p>
      </div>
    </div>
  );
};
