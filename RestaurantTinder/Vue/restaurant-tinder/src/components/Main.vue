<template>
 <div >
    <b-carousel
      id="carousel-1"
      :interval="0"
      background="#ababab"
      img-width="980"
      style="text-shadow: 1px 1px 2px #333;"
    
    >
      <!-- Slide with blank fluid image to maintain slide aspect ratio -->
      <b-carousel-slide 
      v-for="item in justResults" :key="item.reference" 
      img-blank img-alt="Blank image"
      
      >
        <p @click="LoadDetails(item.reference)">
          {{item.name}}
        </p>
        <!-- <div>{{item.photos[0].photo_reference}}</div> -->
      </b-carousel-slide>
      <a href="#"  @click="reject"  class="carousel-control-prev"><span  class="carousel-control-prev-icon"></span><span class="sr-only">Previous Slide</span></a>
      <a href="#" @click="like" class="carousel-control-next"><span  class="carousel-control-next-icon"></span><span class="sr-only">Next Slide</span></a>
    </b-carousel>
  </div>
</template>

<script>
import auth from '../auth';
import tinder from '../tinder';
export default {
  props:{
    detail: []
  },
  data() {
    return {
      rejected: [],
      restaurant: [],
      liked: [],      
    };
  },
  methods: {
    reject(event){
        let inputNode = document.getElementsByClassName("carousel-item active");
        let rejectedResturant = inputNode[0].innerText;
        let testwords = tinder.getRejected();
         if(event){
           tinder.destroyRejected();
           if(!this.rejected.includes(rejectedResturant)){
           this.rejected.push(rejectedResturant)
           }
           let final = this.rejected.concat(testwords);
           
           tinder.saveRejected(final)
           inputNode[0].nextElementSibling.className = "carousel-item active";
           inputNode[0].className = "carousel-item";
          }
   },
   like(event){
      let inputNode = document.getElementsByClassName("carousel-item active");
      let likedResturant = inputNode[0].innerText;
      let testwords = tinder.getLiked();
         if(event){
           tinder.destroyLiked();
           if(!this.liked.includes(likedResturant)){
           this.liked.push(inputNode[0].innerText)
           }
           let final = this.rejected.concat(testwords);
           tinder.saveLiked(final)
           inputNode[0].nextElementSibling.className = "carousel-item active";
           inputNode[0].className = "carousel-item";
          }
   },

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
},
LoadDetails(vm){

  fetch(`${process.env.VUE_APP_REMOTE_API}/api/main/details/?placeId=${vm}`, {
        method: 'get',
        headers: {
          "Content-Type": "application/json",
          Authorization: 'Bearer ' + auth.getToken(),
        },
        credentials: 'same-origin',
      })
        .then((response) => {
          return response.json();
        })
        .then((data) => {
          // this.detail = data;
          let detail = data.result;
          this.$router.push({name: 'details', params: {detail:detail}});
        })
        .catch((err) => console.error(err));
    }


  },
  computed: { 
      justResults(){
          
       let newarray = [];
       for(let i = 0; i < this.restaurant.length; i++){
           newarray = newarray.concat(this.restaurant[i].results)
         }
         let wordsToRemove = tinder.getRejected();
         if(wordsToRemove === null){
            return newarray;
         }
         let filteredKeywords = newarray.filter((word) => !wordsToRemove.includes(word.name));
         return filteredKeywords;
     }, 
  },
   created() {
  if(tinder.getRestaurant() === null){
    fetch(`${process.env.VUE_APP_REMOTE_API}/api/main/search`, {
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
        this.shuffle(this.restaurant)
        tinder.saveRestaurant(this.restaurant)
      })
      .catch((err) => console.error(err));
     }
     if(tinder.getRestaurant() != null){
       this.restaurant = tinder.getRestaurant();
     }
  },
  
}
</script>

<style>
.carousel-inner img {
  height: 88vh;
}
.carousel-control-next-icon {
  background-image: url("../assets/checkt.png")!important;
  width: 50px !important;
  height: 50px !important;
}

.carousel-control-prev-icon {
  background-image: url("../assets/xt.png")!important;
  width: 50px !important;
  height: 50px !important;
}

</style>
