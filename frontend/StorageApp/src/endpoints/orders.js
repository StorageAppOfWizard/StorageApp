import Orders from '../pages/orders';
import { getOrders, getMyOrders, createOrders, approveOrder, rejectOrder } from './../services/orderService';

export const orderEndpoint = {

  OrdersGet: {
    fn: getOrders,
  },

  OrdersMyOrdersGet: {
    fn: getMyOrders,
  },

  OrdersCreate: {
    fn: createOrders,
    isMutation: true,
  },

  OrdersApprove: {
    fn: approveOrder,
    isMutation: true,
  },

  OrdersReject: {
    fn: rejectOrder,
    isMutation: true,
  },

};
