<template>
 <div>
    <b-carousel
      id="carousel-1"
      v-model="slide"
      :interval="0"
      controls
      indicators
      background="#ababab"
      img-width="1024"
      img-height="480"
      style="text-shadow: 1px 1px 2px #333;"
      @sliding-start="onSlideStart"
      @sliding-end="onSlideEnd"
    >


      <!-- Slide with blank fluid image to maintain slide aspect ratio -->
      <b-carousel-slide v-for="item in justResults" :key="item.name"  img-blank img-alt="Blank image">
        <p>
          {{item.name}}
        </p>
      </b-carousel-slide>
    </b-carousel>
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
  methods: {
    shuffle(array) {
        var currentIndex = array.length, temporaryValue, randomIndex;

        // While there remain elements to shuffle...
        while (0 !== currentIndex) {

          // Pick a remaining element...
          randomIndex = Math.floor(Math.random() * currentIndex);
          currentIndex -= 1;

          // And swap it with the current element.
          temporaryValue = array[currentIndex];
          array[currentIndex] = array[randomIndex];
          array[randomIndex] = temporaryValue;
     } 
      return array;
}

  },
  computed: { 
        justResults(){
       let newarray = [];
       for(var i = 0; i < this.restaurant.length; i++){
           newarray = newarray.concat(this.restaurant[i].results)
         }
         return this.shuffle(newarray);
     }, 
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
        this.restaurant = data;
      })
      .catch((err) => console.error(err));
  },
  
}
</script>

<style>

</style>
