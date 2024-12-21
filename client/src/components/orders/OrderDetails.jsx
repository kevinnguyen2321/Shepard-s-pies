import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import {
  assignDriverToOrder,
  getOrderById,
  removePizzaFromOrder,
} from '../../managers/orderManager';
import './OrderDetails.css';
import { getAllUserProfiles } from '../../managers/employeeManager';

export const OrderDetails = () => {
  const [order, setOrder] = useState({});
  const [employees, setEmployees] = useState([]);
  const [driverId, setDriverId] = useState(null);

  const { orderId } = useParams();

  useEffect(() => {
    getOrderById(orderId).then((data) => setOrder(data));

    getAllUserProfiles().then((data) => setEmployees(data));
  }, [orderId]);

  const navigate = useNavigate();

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

  const handleAddPizzaBtnClick = () => {
    navigate(`/orders/${orderId}/add-pizza`);
  };

  const handleRemovePizzaBtnClick = (pizzaId) => {
    removePizzaFromOrder(orderId, pizzaId).then(() =>
      getOrderById(orderId).then((data) => setOrder(data))
    );
  };

  const handleOnChangeDriver = (e) => {
    if (e.target.value === '') {
      setDriverId(null);
      return;
    } else {
      setDriverId(parseInt(e.target.value));
    }
  };

  const handleAssignDriverBtnClick = () => {
    assignDriverToOrder(orderId, driverId).then(() =>
      getOrderById(orderId).then((data) => setOrder(data))
    );
  };

  const handleEditPizzaBtnClick = (pizzaId) => {
    navigate(`/orders/${orderId}/edit-pizza/${pizzaId}`);
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
        {!order.driverId && (
          <div>
            <select onChange={handleOnChangeDriver}>
              <option value="">Select a Driver</option>
              {employees.map((driver) => (
                <option key={driver.id} value={driver.id}>
                  {driver.firstName} {driver.lastName}
                </option>
              ))}
            </select>
            <div>
              <button onClick={handleAssignDriverBtnClick}>
                Assign Driver
              </button>
            </div>
          </div>
        )}

        {order.driver && (
          <>
            <p>Delivery Driver: {order.driver.firstName}</p>
            <p>Delivery Fee: $5.00</p>
          </>
        )}
        <p>Tip: {order.tip ? formatToDollar(order.tip) : 'No Tip'}</p>

        <div>
          <div className="pizza-section-wrapper">
            <h4>Pizzas</h4>
            <button onClick={handleAddPizzaBtnClick}>Add Pizza</button>
          </div>

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
                      <li key={topping.id}>{topping.topping.name}(.50 ea)</li>
                    ))}
                  </ul>
                </div>

                <div>
                  <h5>Pizza Total</h5>
                  <p>{formatToDollar(pizza.totalWithToppings)}</p>
                </div>
                <div>
                  <button onClick={() => handleEditPizzaBtnClick(pizza.id)}>
                    Edit Pizza
                  </button>
                  <button onClick={() => handleRemovePizzaBtnClick(pizza.id)}>
                    Remove Pizza
                  </button>
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
