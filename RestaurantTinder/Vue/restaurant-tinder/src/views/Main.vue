<template>
<test>
<h1>{{restaurant.results[2].vicinity}}</h1>

</test>
</template>

<script>
// import auth from '../auth';
import Test from '../layouts/DefaultLayout.vue'
export default {
 name: 'mainPage',
  components: {
    Test,
  },
  data() {
    return {
      restaurant: [],
    };
  },
   created() {
    fetch(`${process.env.VUE_APP_REMOTE_API}/api/main/`, {
      method: 'GET'
    })
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        this.restaurant = data;
      })
      .catch((err) => console.error(err));
  },
}
</script>

<style>

</style>
