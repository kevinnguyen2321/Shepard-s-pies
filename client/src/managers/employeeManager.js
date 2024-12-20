const _apiURl = '/api/UserProfile';

export const getAllUserProfiles = () => {
  return fetch(_apiURl).then((response) => response.json());
};
