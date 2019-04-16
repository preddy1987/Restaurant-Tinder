<template>
<div>
   <h1>Restaurants</h1>
        <ul>
            <li v-for="item in restaurant" :key="item.id">{{item.results.name}} </li>
        </ul>
</div>
</template>

<script>
import auth from '../auth';
export default {
  data() {
    return {
      restaurant: [],
    };
  },
   created() {
    fetch(`${process.env.VUE_APP_REMOTE_API}/api/main/`, {
      method: 'GET',
      headers: {
        Authorization: 'Bearer ' + auth.getToken(),
      },
      credentials: 'same-origin',
    })
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        this.restaurant += data;
      })
      .catch((err) => console.error(err));
  },
}
</script>

<style>

</style>
