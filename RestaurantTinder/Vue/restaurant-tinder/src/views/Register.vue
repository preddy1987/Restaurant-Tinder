<template>
 <test>
      <div id="modal-wrapper" class="modal" @click='documentClick'>
     <form class="modal-content animate" @submit.prevent="login">
      <div class="imgcontainer">
        <router-link class="close"  title="Close PopUp" :to="{ name: 'landing' }">&times;</router-link>
        <h1 style="text-align:center">Please Sign Up</h1>
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
        <label for="firstname" class="sr-only">Firstname</label>
        <input
          type="text"
          id="firstname"
          class="form-control"
          placeholder="Firstname"
          v-model="user.firstname"
          required
          autofocus
        />
        <label for="lastname" class="sr-only">lastname</label>
        <input
          type="text"
          id="lastname"
          class="form-control"
          placeholder="Lastname"
          v-model="user.lastname"
          required
          autofocus
        />
      <label for="email" class="sr-only">Email</label>
        <input
          type="email"
          id="email"
          class="form-control"
          placeholder="Email"
          v-model="user.email"
          required
          autofocus
        />
        <label for="zipcode" class="sr-only">lastname</label>
        <input
          type="text"
          id="zipcode"
          class="form-control"
          placeholder="zipcode"
          v-model="user.zipCode"
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
        <input
          type="password"
          id="confirmPassword"
          class="form-control"
          placeholder="Confirm Password"
          v-model="user.confirmPassword"
          required
        />

      <button @click.stop.prevent="RegisterUser" class="btn btn-lg btn-primary btn-block" type="submit">
        Create Account
      </button>
    </div>
    </form>
   </div>
</test>
</template>

<script>
import Test from '../layouts/DefaultLayout.vue'
export default {
  name: 'register',
  components: {
    Test,
  },
  data() {
    return {
      user: {
        username: '',
        firstname: '',
        lastname: '',
        email: '',
        zipCode: '',  
        password: '',
        confirmPassword: '',
      },
      registrationErrors: false,
    };
  },
  methods: {
    RegisterUser() {
      fetch(`${process.env.VUE_APP_REMOTE_API}/api/register`, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.user),
      })
        .then((response) => {
          if (response.ok) {
            // this.$router.push({ path: '/login'});
          } else {
            this.registrationErrors = true;
          }
        })

        .then((err) => console.error(err));
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
input[type=text], input[type=password], input[type=email] {
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
    top: 10px;
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
@keyframes zoom {
    from {transform: scale(0)} 
    to {transform: scale(1)}
}

</style>
