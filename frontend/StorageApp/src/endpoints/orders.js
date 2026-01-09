import Orders from '../pages/orders';
import { getOrders, getMyOrders } from './../services/orderService';

export const orderEndpoint = {

  OrdersGet: {
    fn: getOrders,
  },

  OrdersMyOrdersGet: {
    fn: getMyOrders,
  },

};
