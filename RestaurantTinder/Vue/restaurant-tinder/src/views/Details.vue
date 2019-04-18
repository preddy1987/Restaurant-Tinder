<template>
<layout>
    <div id="details">
     <div id="modal-wrapper" class="modal" @click='documentClick'>
      <div class="modal-content animate">
       <div class="imgcontainer">
        <router-link class="close"  title="Close PopUp" :to="{ name: 'landing' }">&times;</router-link>
       </div>
        <h4 style="text-align:center">{{$attrs.detail.name}}</h4>
        <div>Address: {{$attrs.detail.formatted_address}}</div>
        <div>Phone Number: {{$attrs.detail.formatted_phone_number}}</div>
        <ul>
            <li v-for="item in $attrs.detail.opening_hours.weekday_text" :key="item">{{item}}</li>
        </ul>
      </div>
     </div>
    </div>
</layout>
</template>

<script>
// import auth from '../auth';
import Layout from '../layouts/DefaultLayout.vue'
export default {
    name: 'Details',
    components:{
        Layout
    },
    data(){
        return{
        }
    },
    methods: {
        documentClick(e){
            let el =  document.getElementById('modal-wrapper');
            if ( e.target == el ) {
            this.$router.push('/');
            }
        }
    }
}
</script>

<style scoped>
ul {
list-style-type: none;
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
</style>