import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getPizzaById, updatePizza } from '../../managers/pizzaManager';
import { getAllSauces } from '../../managers/sauceManager';
import { getAllCheeses } from '../../managers/cheeseManager';
import { getAllToppings } from '../../managers/toppingManager';

export const EditPizza = () => {
  const [pizza, setPizza] = useState({});
  const [sauces, setSauces] = useState([]);
  const [cheeses, setCheeses] = useState([]);
  const [toppings, setToppings] = useState([]);
  const { pizzaId } = useParams();

  const navigate = useNavigate();

  useEffect(() => {
    getPizzaById(pizzaId).then((data) => setPizza(data));

    getAllSauces().then((data) => setSauces(data));
    getAllCheeses().then((data) => setCheeses(data));
    getAllToppings().then((data) => setToppings(data));
  }, [pizzaId]);

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
    const toppingId = parseInt(e.target.value); // Get the topping ID from the checkbox value
    const pizzaCopy = { ...pizza };

    if (e.target.checked) {
      // Add new pizzaTopping object if checked
      pizzaCopy.toppings = [...pizzaCopy.toppings, { toppingId }];
    } else {
      // Remove the pizzaTopping object if unchecked
      pizzaCopy.toppings = pizzaCopy.toppings.filter(
        (topping) => topping.toppingId !== toppingId
      );
    }

    setPizza(pizzaCopy);
  };

  const handleUpdateBtnClick = (e) => {
    e.preventDefault();
    let size = '';
    if (pizza.price === 10) {
      size = 'Small';
    } else if (pizza.price === 12) {
      size = 'Medium';
    } else if (pizza.price === 15) {
      size = 'Large';
    }

    const mappedToppings = pizza.toppings.map((topping) => topping.toppingId);
    const finalObj = {
      size,
      price: pizza.price,
      cheeseId: pizza.cheeseId,
      sauceId: pizza.sauceId,
      orderId: pizza.orderId,
      toppingIds: mappedToppings,
    };

    updatePizza(pizzaId, finalObj).then(() =>
      navigate(`/orders/${pizza.orderId}`)
    );
  };

  return (
    <div>
      <form>
        <label>Size</label>
        <select name="price" onChange={handleOnChangePizza} value={pizza.price}>
          <option value="">--Select a size--</option>
          <option value={10.0}>Small (10") - $10.00</option>
          <option value={12.0}>Medium (14") - $12.00</option>
          <option value={15.0}>Large (18") - $15.00</option>
        </select>

        <label>Sauces</label>
        <select
          name="sauceId"
          onChange={handleOnChangePizza}
          value={pizza.sauceId}
        >
          <option value="">--Select a sauce--</option>
          {sauces.map((sauce) => (
            <option key={sauce.id} value={sauce.id}>
              {sauce.name}
            </option>
          ))}
        </select>
        <label>Cheeses</label>

        <select
          name="cheeseId"
          onChange={handleOnChangePizza}
          value={pizza.cheeseId}
        >
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
                  checked={
                    pizza.toppings?.some((pt) => pt.toppingId === topping.id) ||
                    false
                  }
                />
                <label>{topping.name}</label>
              </div>
            );
          })}
        </div>
        <button onClick={handleUpdateBtnClick}>Update Pizza</button>
      </form>
    </div>
  );
};
