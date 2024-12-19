const _apiUrl = '/api/order';

export const getAllOrders = () => {
    return fetch(_apiUrl)
        .then(response => response.json());
}

export const getOrderById = (id) => {
    return fetch(`${_apiUrl}/${id}`)
        .then(response => response.json());
}