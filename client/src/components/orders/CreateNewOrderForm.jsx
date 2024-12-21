import { useEffect } from 'react';
import { useState } from 'react';
import { getAllUserProfiles } from '../../managers/employeeManager';
import { getAllSauces } from '../../managers/sauceManager';
import { getAllCheeses } from '../../managers/cheeseManager';
import { getAllToppings } from '../../managers/toppingManager';
import './CreateNewOrderForm.css';
import {
  addPizzaToOrder,
  createOrderAndAddPizza,
} from '../../managers/orderManager';
import { useNavigate, useParams } from 'react-router-dom';

export const CreateNewOrderForm = ({ loggedInUser }) => {
  const [employees, setEmployees] = useState([]);
  const [sauces, setSauces] = useState([]);
  const [cheeses, setCheeses] = useState([]);
  const [toppings, setToppings] = useState([]);
  const [order, setOrder] = useState({
    userProfileId: loggedInUser?.id,
  });
  const [pizza, setPizza] = useState({ toppingIds: [] });
  const [isDelivery, setIsDelivery] = useState(false);

  const { orderId } = useParams();

  const navigate = useNavigate();

  useEffect(() => {
    getAllUserProfiles().then((data) => setEmployees(data));
    getAllSauces().then((data) => setSauces(data));
    getAllCheeses().then((data) => setCheeses(data));
    getAllToppings().then((data) => setToppings(data));
  }, []);

  const handleOnChangePizza = (e) => {
    const pizzaCopy = { ...pizza };

    if (e.target.name === 'sauceId') {
      pizzaCopy[e.target.name] = parseInt(e.target.value);
    } else if (e.target.name === 'cheeseId') {
      pizzaCopy[e.target.name] = parseInt(e.target.value);
    } else if (e.target.name === 'price') {
      pizzaCopy[e.target.name] = parseFloat(e.target.value);
    } else {
      pizzaCopy[e.target.name] = e.target.value;
    }

    setPizza(pizzaCopy);
  };

  const handleToppingChange = (e) => {
    const pizzaCopy = { ...pizza };
    const toppingId = parseInt(e.target.value);

    if (e.target.checked) {
      pizzaCopy.toppingIds = [...pizzaCopy.toppingIds, toppingId];
    } else {
      pizzaCopy.toppingIds = pizzaCopy.toppingIds.filter(
        (id) => id !== toppingId
      );
    }

    setPizza(pizzaCopy);
  };

  const handleOrderChange = (e) => {
    const orderCopy = { ...order };
    if (e.target.name === 'tip') {
      if (e.target.value === '') {
        orderCopy[e.target.name] = null;
      } else {
        orderCopy[e.target.name] = parseFloat(e.target.value);
      }
    } else if (e.target.name === 'driverId') {
      if (e.target.value === '') {
        orderCopy[e.target.name] = null;
      } else {
        orderCopy[e.target.name] = parseInt(e.target.value);
      }
    }
    setOrder(orderCopy);
  };

  const toggleIsDelivery = (event) => {
    event.preventDefault();
    setIsDelivery(!isDelivery);
  };

  const handleSubmitOrderClick = async (event) => {
    event.preventDefault();
    if (!pizza.price || !pizza.sauceId || !pizza.cheeseId) {
      alert('Please select a size, sauce, and cheese for your pizza');
      return;
    } else {
      if (pizza.price === 10.0) {
        pizza.size = 'Small';
      } else if (pizza.price === 12.0) {
        pizza.size = 'Medium';
      } else if (pizza.price === 15.0) {
        pizza.size = 'Large';
      }
      console.log('Order:', order);
      console.log('Pizza:', pizza);
      try {
        // Wait for the async function to complete
        await createOrderAndAddPizza(order, pizza);

        // After the order is successfully created, navigate to another page
        navigate('/orders'); // Navigate to the 'orders' page or wherever you'd like
      } catch (error) {
        console.error('Error creating order:', error);
        alert('There was an issue with your order. Please try again.');
      }
    }
  };

  const handleAddPizzaToOrderClick = (orderId, pizza) => {
    event.preventDefault();

    if (!pizza.sauceId || !pizza.cheeseId || !pizza.price) {
      alert('Please select a sauce and cheese for your pizza');
      return;
    } else {
      if (pizza.price === 10.0) {
        pizza.size = 'Small';
      } else if (pizza.price === 12.0) {
        pizza.size = 'Medium';
      } else if (pizza.price === 15.0) {
        pizza.size = 'Large';
      }
      addPizzaToOrder(orderId, pizza).then(() =>
        navigate(`/orders/${orderId}`)
      );
    }
  };

  return (
    <form>
      <label>Size</label>
      <select name="price" onChange={handleOnChangePizza}>
        <option value="">--Select a size--</option>
        <option value={10.0}>Small (10") - $10.00</option>
        <option value={12.0}>Medium (14") - $12.00</option>
        <option value={15.0}>Large (18") - $15.00</option>
      </select>

      <label>Sauces</label>
      <select name="sauceId" onChange={handleOnChangePizza}>
        <option value="">--Select a sauce--</option>
        {sauces.map((sauce) => (
          <option key={sauce.id} value={sauce.id}>
            {sauce.name}
          </option>
        ))}
      </select>
      <label>Cheeses</label>

      <select name="cheeseId" onChange={handleOnChangePizza}>
        <option value={null}>--Select a cheese--</option>
        {cheeses.map((cheese) => (
          <option key={cheese.id} value={cheese.id}>
            {cheese.name}
          </option>
        ))}
      </select>

      <div>
        <label>Toppings</label>
        {toppings.map((topping) => {
          return (
            <div key={topping.id}>
              <input
                type="checkbox"
                value={topping.id}
                onChange={handleToppingChange}
              />
              <label>{topping.name}</label>
            </div>
          );
        })}
      </div>

      <div>
        {!orderId && (
          <button
            className={isDelivery ? 'delivery' : ''}
            onClick={toggleIsDelivery}
          >
            Delivery
          </button>
        )}
        {isDelivery && (
          <div>
            <label>Driver</label>
            <select name="driverId" onChange={handleOrderChange}>
              <option value="">--Select a driver--</option>
              {employees.map((employee) => (
                <option key={employee.id} value={employee.id}>
                  {employee.firstName} {employee.lastName}
                </option>
              ))}
            </select>
          </div>
        )}
        <div>
          {!orderId && (
            <>
              <label>Tip</label>
              <input
                id="tip"
                type="number"
                name="tip"
                step="0.01"
                min="0"
                placeholder="Enter tip amount"
                onChange={handleOrderChange}
              />{' '}
            </>
          )}
          <div>
            {orderId ? (
              <button
                onClick={() => handleAddPizzaToOrderClick(orderId, pizza)}
              >
                Add Pizza to Order
              </button>
            ) : (
              <button onClick={handleSubmitOrderClick}>Submit Order</button>
            )}
          </div>
        </div>
      </div>
    </form>
  );
};
