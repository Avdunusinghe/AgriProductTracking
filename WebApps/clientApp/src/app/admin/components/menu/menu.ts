import { Menu } from './menu.model'; 

export const menuItems = [ 
    new Menu (10, 'Dashboard', '/admin', null, 'dashboard', null, false, 0),
    new Menu (20, 'Product', null, null, 'grid_on', null, true, 0),   
    new Menu (22, 'Products List', '/admin/product/product-list', null, 'list', null, false, 20), 
    new Menu (23, 'Product Detail', '/admin/product/product-detail', null, 'add_circle_outline', null, false, 20),
    new Menu (24, 'User', null, null, 'group_add', null, true, 0),
    new Menu (26, 'User List', '/admin/user/user-list', null, 'list', null, false, 24),   
    new Menu (27, 'User Detail', '/admin/user/user-detail', null, 'add_circle_outline', null, false, 24), 
    new Menu (28, 'Delivery-Service', null, null, 'supervisor_account', null, true, 0),
    new Menu (30, 'Delivery-Service List', '/admin/deliveryservice/deliveryservice-list', null, 'list', null, false, 28),   
    new Menu (32, 'Delivery-Service Detail', '/admin/deliveryservice/deliveryservice-detail', null, 'add_circle_outline', null, false, 28), 
   
]