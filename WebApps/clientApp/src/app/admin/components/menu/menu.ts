import { Menu } from './menu.model'; 

export const menuItems = [ 
    new Menu (10, 'Dashboard', '/admin', null, 'dashboard', null, false, 0),
    new Menu (20, 'Product', null, null, 'grid_on', null, true, 0),   
    new Menu (22, 'Products List', '/admin/product/product-list', null, 'list', null, false, 20), 
    new Menu (23, 'Product Detail', '/admin/product/product-detail', null, 'add_circle_outline', null, false, 20),  
   
   
]