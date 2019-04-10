import Vue from 'vue';
import Router from 'vue-router';
import Login from '@/views/Login';

Vue.use(Router);

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Login,
    }
  ],
})


export default router;