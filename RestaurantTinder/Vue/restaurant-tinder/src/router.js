import Vue from 'vue';
import Router from 'vue-router';
import Login from './views/Login.vue'
import Register from './views/Register.vue'
import Landing from './views/Landing.vue'
import UserPreferences from './views/UserPreferences.vue'

Vue.use(Router);

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'landing',
      component: Landing,
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
    },
    {
        path: '/register',
        name: 'register',
        component: Register,
    },
    {
      path: '/userpreferences',
      name: 'userpreferences',
      component: UserPreferences,
    }
  ],
})

export default router;