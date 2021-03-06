import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules  } from '@angular/router'; 

import { PagesComponent } from './pages/pages.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';

export const routes: Routes = [
    { 
        path: '', 
        component: PagesComponent, children: [
            { path: '',
             loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule) 
            },
            { 
                path: 'authentication', 
                loadChildren: () => import('./pages/authentication/authentication.module').then(m => m.AuthenticationModule), 
                data: { breadcrumb: 'Authentication' } 
            },
            { 
                path: 'contact', 
                loadChildren: () => import('./pages/contact/contact.module').then(m => m.ContactModule), 
                data: { breadcrumb: 'Contact' } 
            }, 
            { 
                path: 'products', 
                loadChildren: () => import('./pages/products/products.module').then(m => m.ProductsModule), 
                data: { breadcrumb: 'All Products' } 
            },
            { 
                path: 'cart', 
                loadChildren: () => import('./pages/cart/cart.module').then(m => m.CartModule), 
                data: { breadcrumb: 'Cart' } 
            },  
            { 
                path: 'checkout', 
                loadChildren: () => import('./pages/checkout/checkout.module').then(m => m.CheckoutModule), 
                data: { breadcrumb: 'Checkout' } 
            },
        ]
    },
    { 
        path: 'landing', 
        loadChildren: () => import('./landing/landing.module').then(m => m.LandingModule)
    },
    { 
        path: 'admin', 
        loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) 
    },
    { 
        path: '**', 
        component: NotFoundComponent 
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes, {
            preloadingStrategy: PreloadAllModules, // <- comment this line for activate lazy load
            relativeLinkResolution: 'legacy',
            // useHash: true
        })
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }