import Vue from 'vue';
import Router from 'vue-router';
import Login from './views/Login.vue'
import Register from './views/Register.vue'

Vue.use(Router);

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'login',
      component: Login,
    },
    {
        path: '/register',
        name: 'register',
        component: Register,
      }
  ],
})

export default router;