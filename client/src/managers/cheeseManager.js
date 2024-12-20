const _apiURl = '/api/cheese';

export const getAllCheeses = () => {
  return fetch(_apiURl).then((response) => response.json());
};
