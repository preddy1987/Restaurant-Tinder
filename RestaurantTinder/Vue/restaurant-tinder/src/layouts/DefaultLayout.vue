<template>
  <div >
    <landing></landing>
    <div>
      <slot />
    </div>
  </div>
</template>

<script>
import Landing from '../views/Landing.vue';


export default {
  name: 'DefaultLayout',
  components: {
    Landing
  },
};
</script>

<style>
</style>