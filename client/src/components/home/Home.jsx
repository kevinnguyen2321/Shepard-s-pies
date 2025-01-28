import { useNavigate } from 'react-router-dom';
import './Home.css';
import PizzaImage from '../../assets/pizza.PNG';

export const Home = () => {
  const navigate = useNavigate();

  const handleNewOrderClick = () => {
    navigate('/orders/new');
  };
  return (
    <div className="main-container">
      <h1>Shepard's Pies</h1>
      <p>Welcome to the Shepard's Pie Shop</p>
      <button onClick={handleNewOrderClick}>New Order</button>
    </div>
  );
};
