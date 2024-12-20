const _apiURl = '/api/topping';

export const getAllToppings = () => {
  return fetch(_apiURl).then((response) => response.json());
};
