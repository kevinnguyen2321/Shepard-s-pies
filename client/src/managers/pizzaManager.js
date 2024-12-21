const _apiUrl = '/api/pizza';

export const updatePizza = (pizzaId, pizza) => {
  return fetch(`${_apiUrl}/${pizzaId}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(pizza),
  });
};

export const getPizzaById = (pizzaId) => {
  return fetch(`${_apiUrl}/${pizzaId}`).then((res) => res.json());
};
