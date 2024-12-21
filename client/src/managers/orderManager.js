const _apiUrl = '/api/order';

export const getAllOrders = () => {
  return fetch(_apiUrl).then((response) => response.json());
};

export const getOrderById = (id) => {
  return fetch(`${_apiUrl}/${id}`).then((response) => response.json());
};

export const createOrder = (order) => {
  return fetch(_apiUrl, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(order),
  }).then((response) => response.json());
};

// Function to add a pizza to an order
export const addPizzaToOrder = (orderId, pizza) => {
  return fetch(`${_apiUrl}/${orderId}/add-pizza`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(pizza),
  }).then((response) => response.json());
};

// Create order and add pizza
export const createOrderAndAddPizza = async (order, pizza) => {
  try {
    // Step 1: Create the order
    const createdOrder = await createOrder(order);

    // Ensure the order was created successfully
    if (!createdOrder || !createdOrder.id) {
      throw new Error('Failed to create order');
    }

    const orderId = createdOrder.id;

    // Step 2: Add the pizza to the created order
    const addedPizza = await addPizzaToOrder(orderId, pizza);

    // Return the result
    return {
      order: createdOrder,
      pizza: addedPizza,
    };
  } catch (error) {
    console.error('Error creating order or adding pizza:', error);
    throw error; // Re-throw to handle it at a higher level if needed
  }
};

export const removePizzaFromOrder = (orderId, pizzaId) => {
  return fetch(`${_apiUrl}/${orderId}/remove-pizza?pizzaId=${pizzaId}`, {
    method: 'PUT',
  }).then((response) => {
    if (!response.ok) {
      throw new Error('Failed to remove pizza from order');
    } else {
      return { success: true, message: 'Pizza removed successfully' };
    }
  });
};

export const assignDriverToOrder = (orderId, driverId) => {
  return fetch(`${_apiUrl}/${orderId}/assign-driver?driverId=${driverId}`, {
    method: 'PUT',
  }).then((response) => {
    if (!response.ok) {
      throw new Error('Failed to remove pizza from order');
    } else {
      return { success: true, message: 'Pizza removed successfully' };
    }
  });
};

export const deleteOrder = (id) => {
  return fetch(`${_apiUrl}/${id}`, {
    method: 'DELETE',
  }).then((response) => {
    if (!response.ok) {
      throw new Error('Failed to delete order');
    } else {
      return { success: true, message: 'Order deleted successfully' };
    }
  });
};
