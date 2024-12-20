const _apiURl = '/api/sauce';

export const getAllSauces = () => {
  return fetch(_apiURl).then((response) => response.json());
};
