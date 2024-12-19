import { useNavigate } from 'react-router-dom';

export const Home = () => {
  const navigate = useNavigate();

  const handleNewOrderClick = () => {
    console.log('hi');
  };
  return (
    <div>
      <h1>Shepard's Pies</h1>
      <p>Welcome to the Shepard's Pie Shop</p>
      <button onClick={handleNewOrderClick}>New Order</button>
    </div>
  );
};
