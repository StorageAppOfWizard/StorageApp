import { getOrders } from './../services/orderService';

export const orderEndpoint = {

  OrdersGet: {
    fn: getOrders,
  },

};
