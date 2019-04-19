<template>
<layout >
  <div id="modal-wrapper" class="modal" @click='documentClick'>
    <form id="test2" class="modal-content animate" @submit.prevent="login">
      <div class="imgcontainer">
         <router-link class="close"  title="Close PopUp" :to="{ name: 'landing' }">&times;</router-link>
        <h1 style="text-align:center">Please Sign In</h1>
      </div>
       <div class="alert alert-danger" role="alert" v-if="invalidCredentials">
        Invalid username and password!
      </div>

      <div class="container">
        <label for="username" class="sr-only">Username</label>
          <input
            type="text"
            id="username"
            class="form-control"
            placeholder="Username"
            v-model="user.username"
            required
            autofocus
          />
         <label for="password" class="sr-only">Password</label>
          <input
            type="password"
            id="password"
            class="form-control"
            placeholder="Password"
            v-model="user.password"
            required
          />     
          <button  class="btn btn-primary btn-lg" type="submit">
          Sign in
        </button>
      </div>
      </form>
    </div>
</layout>
</template>

<script>
import auth from '../auth';
import Layout from '../layouts/DefaultLayout.vue'
export default {
  name: 'Login',
  components: {
    Layout,
  },
  data() {
    return {
      user: {
        username: '',
        password: '',
      },
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/login`, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.user),
      })
       .then((response) => {
          if (response.ok) {
            return response.text();
          } else {
            this.invalidCredentials = true;
          }
        })
         .then((token) => {
          if (token != undefined) {
            if (token.includes('"')) {
              token = token.replace(/"/g, '');
            }
            auth.saveToken(token);
            this.$router.push('/');
          }
        })
        .catch((err) => console.error(err));
    },
     documentClick(e){
        let el =  document.getElementById('modal-wrapper');
        if ( e.target == el ) {
          this.$router.push('/');
        }
      },
    },
};
</script>

<style scoped>
 /* test css */
 /* Full-width input fields */
input[type=text], input[type=password] {
    width: 90%;
    padding: 12px 20px;
    margin: 8px 26px;
    display: inline-block;
    border: 1px solid #ccc;
    box-sizing: border-box;
	font-size:16px;
}

/* Set a style for all buttons */
.btn-primary {
    background-color: #ee0979;
    border-color: #ee0979;
    padding: 14px 20px;
    margin: 8px 26px;
    border: none;
    cursor: pointer;
    font-size:20px;
    width: 90%;
}
button:hover {
    opacity: 0.8;
    background-color: #bd0760 !important;
    border-color: #bd0760 !important;
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
    top: 5%;
    width: 100%;
    height: 100%;
    overflow: auto;
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
@media (max-width: 800px) {
  #modal-wrapper {
    position: fixed;
    display: block;
    z-index: 1;
    left: 0;
    top: 10%;
    width: 100%;
    height: 100%;
    overflow: auto;
    background-color: rgba(0,0,0,0.4);
  
  }
  .modal-content {
  background-color: #fefefe;
  margin: 4% auto 15% auto;
  border: 1px solid #888;
  width: 100%;
  height: 100%;
	padding-bottom: 30px;
 }
  
}
@keyframes zoom {
    from {transform: scale(0)} 
    to {transform: scale(1)}
}

</style>