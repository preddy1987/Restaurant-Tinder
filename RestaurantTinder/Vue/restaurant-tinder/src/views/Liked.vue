<template>
<div>
  <nav class="navbar navbar-expand-lg navbar-dark navbar-custom">
    <div class="container">
      <img id="main-logo" src="@/assets/dot.png" alt="">
      <a class="navbar-brand" ><span>Restaurant tinder</span></a>
      <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarResponsive">
        <ul class="navbar-nav ml-auto">
           <li class="nav-item" @click="logout" >
              <a class="nav-link"  href="#">Logout</a>
          </li>
          <li class="nav-item"> 
             <router-link class="nav-link"  :to="{ name: 'userpreferences' }">Preferences</router-link>
          </li>
          <li class="nav-item"> 
             <router-link class="nav-link"  :to="{ name: 'liked' }">Likes</router-link>
          </li>
           <li class="nav-item"> 
             <router-link class="nav-link" :to="{ name: 'landing' }">Home</router-link>
          </li>
        </ul>
      </div>
    </div>
  </nav>

   <b-carousel
      id="carousel-1"
      :interval="5000"
      controls
      style="text-shadow: 1px 1px 2px #333;"
    
    >
 
      <!-- Slide with blank fluid image to maintain slide aspect ratio -->
      <b-carousel-slide 
      v-for="item in test" :key="item.reference"
      >
         <img
          slot="img"
          class="main-img"
          :src="getPhoto(item.photos[0].photo_reference)"
          alt="image slot"
          @click="LoadDetails(item.reference)"
        >
      
        <h2  @click="LoadDetails(item.reference)">
          {{item.name}}
        </h2>
      </b-carousel-slide>
    </b-carousel>
    <footer class="py-5 bg-black">
    <div class="container">
      <p class="m-0 text-center text-white small">Copyright &copy; Your Website 2019</p>
    </div>
  </footer>
</div>
</template>

<script>
import auth from '../auth';
import tinder from '../tinder'
export default {

  data() {
    return {
      restaurant: []
    };
  },
  computed: {
     test(){
         let newarray = [];
       for(let i = 0; i < this.restaurant.length; i++){
           newarray = newarray.concat(this.restaurant[i].results)
         }
         let wordsToRemove = tinder.getLiked();
         if(wordsToRemove === null){
            return newarray;
         }
         let filteredKeywords = newarray.filter((word) => wordsToRemove.includes(word.name));
         return filteredKeywords;
     }
  },  
  methods: {
      logout() {
      auth.logout();
      tinder.destroyRejected();
      tinder.destroyRestaurant();
      tinder.destroyLiked();
      this.$router.go('/');
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
    },
    getPhoto(string){
     return `https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=${string}&key=AIzaSyDDHeRZd4LXtzzV41AN2CiZPXEA7R8Y3Tg`
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
        tinder.saveRestaurant(this.restaurant )
      })
      .catch((err) => console.error(err));
     }
     if(tinder.getRestaurant() != null){
       this.restaurant = tinder.getRestaurant();
     }
  },

}

</script>

<style scoped>
#main-logo{
    width: 5%;
}
.carousel {
  background: -webkit-gradient(linear, left bottom, left top, from(#ff6a00), to(#ee0979));
  background: linear-gradient(0deg, #ff6a00 0%, #ee0979 100%);
  background-repeat: no-repeat;
  background-position: center center;
  background-attachment: scroll;
  background-size: cover;
}


.navbar-brand span{
    color: white;
}


.navbar-custom {
  padding-top: 1rem;
  padding-bottom: 1rem;
  background-color: rgba(0, 0, 0, .8);
}

.navbar-custom .navbar-brand {
  text-transform: uppercase;
  font-size: 1rem;
  letter-spacing: 0.1rem;
  font-weight: 700;
  width: 80%;
}

.navbar-custom .navbar-nav .nav-item .nav-link {
  text-transform: uppercase;
  font-size: 0.8rem;
  font-weight: 700;
  letter-spacing: 0.1rem;
}


.navbar-custom .navbar-brand{
    width: 20%;
}

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
#main-img img{
  width: 100%;
}
.carousel-caption{
  background: #333333;
}
.main-img{
    width: 60% !important;
    margin-left: 20% !important;
    cursor: pointer;
  }
h2{
  cursor:pointer;
}
.bg-black {
  background-color: #000 !important;
  padding-bottom: 1rem!important;
  padding-top: 1rem!important;
}
</style>
