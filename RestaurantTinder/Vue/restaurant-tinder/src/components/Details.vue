<template>
    <div id="details">
     <div id="modal-wrapper" class="modal">
      <div class="modal-content animate">
       <div class="imgcontainer">
        <span @click="$emit('show-details');" class="close" title="Close PopUp">&times;</span>
       </div>
        <h4 style="text-align:center">{{this.detail.name}}</h4>
        <b-carousel
            id="carousel-detail"
            :interval= "2000"
            indicators
            controls
            style="text-shadow: 1px 1px 2px #333;"   
        >
        <!-- Slide with blank fluid image to maintain slide aspect ratio -->
            <b-carousel-slide 
            v-for="item in this.detail.photos" :key="item.photo_reference"
            >
            
       <img
          slot="img"
          class="main-img"
          :src="getPhoto(item.photo_reference)"
          alt="image slot"
        >
            </b-carousel-slide>
        </b-carousel>
        <div class="info">Address: {{detail.formatted_address}}</div>
        <div class="info">Phone Number: {{detail.formatted_phone_number}}</div>
        <div class="info">Would you like directions? <a :href="detail.url" target="_blank">Click Here</a></div>
        <ul>
            <h2>Hours of Operation</h2>
            <li class="address-item" v-for="item in detail.opening_hours.weekday_text" :key="item">{{item}}</li>
        </ul>
      </div>
     </div>
    </div>
</template>

<script>
// import auth from '../auth';
export default {
    name: 'Details',
    components:{
    },
    props:{
      detail: Object
    },
    data(){
        return{
        }
    },
    methods: {
 
        getPhoto(string){
            return `https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=${string}&key=AIzaSyDDHeRZd4LXtzzV41AN2CiZPXEA7R8Y3Tg`
        },
    }
}
</script>

<style scoped>

.modal-content{
    color: white;
    background: -webkit-gradient(linear, left bottom, left top, from(#ff6a00), to(#ee0979));
    background: linear-gradient(0deg, #ff6a00  0%, #ee0979  100%);
    background-repeat: no-repeat;
    background-position: center center;
    background-attachment: scroll;
    background-size: cover;
    overflow: auto;
}
ul {
    list-style-type: none;
}
.info{
    margin-left: 20px;
}
.button{
    width: 150px;
    height: 50px;
    border: white solid 2px;
    font-weight: 600;
}
/* Center the image and position the close button */
.imgcontainer {
    text-align: center;
    margin: 24px 0 12px 0;
    position: relative;
}
.avatar {
    width: 200px;
	height:200px;
    border-radius: 50%;
}
h2{
    margin-top: 10px;
    margin-bottom: -1px;
    font-size: 16px;
}
ul li{
    font-size: 12px;
}

/* The Modal (background) */
#modal-wrapper {
    position: fixed;
    display: block;
    z-index: 1;
    left: 0;
    top: 1rem;
    width: 100%;
    height: 100%;
    overflow:hidden;
    background-color: rgba(0,0,0,0.4);
}


/* Modal Content Box */
.modal-content {
  background-color: #fefefe;
  margin: 4% auto 15% auto;
  border: 1px solid #888;
  width: 50%;
  padding-bottom: 30px;
}

/* The Close Button (x) */
.close {
    position: absolute;
    right: 25px;
    top: 0;
    color: #000;
    font-size: 35px;
    font-weight: bold;
}
.close:hover,.close:focus {
    color: red;
    cursor: pointer;
}

/* Add Zoom Animation */
.animate {
    animation: zoom 0.6s
}
#carousel-detail{
    align-self: center;
    text-align: center;
    width: 400px;
    height: 350px;
}
.main-img{
    width:300px;
    height: 300px;
}
</style>