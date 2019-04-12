<template>
<div>
  <nav class="navbar navbar-expand-lg navbar-dark navbar-custom fixed-top">
    <div class="container">
      <a class="navbar-brand" ><img id="main-logo" src="@/assets/dot.png" alt=""><span>Restaurant tinder</span></a>
      <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarResponsive">
        <ul class="navbar-nav ml-auto">
          <li class="nav-item">
             <router-link class="nav-link" :to="{ name: 'register' }">Sign Up</router-link>
          </li>
        </ul>
      </div>
    </div>
  </nav>
  <header class="masthead text-center text-white">
    <div class="masthead-content">
      <div class="container">
        <h1 class="masthead-heading mb-0">Hunger is a terrible</h1>
        <h1 class="masthead-subheading mb-0">thing to waste</h1>
         <router-link class="btn btn-primary btn-xl rounded-pill mt-5" :to="{ name: 'login' }">Log In</router-link>
      </div>
    </div>
  </header>
 
  <footer class="py-5 bg-black">
    <div class="container">
      <p class="m-0 text-center text-white small">Copyright &copy; Your Website 2019</p>
    </div>
  </footer>
    </div>
</template>

<script>
export default {

}
</script>

<style scoped>
#main-logo{
    width: 8%;
}
body {
  font-family: 'Lato';
}

h1,
h2,
h3,
h4,
h5,
h6 {
  font-family: 'Catamaran';
  font-weight: 800 !important;
  -webkit-text-stroke: 1px rgb(138, 105, 105);
}

.btn-xl {
  text-transform: uppercase;
  padding: 1.5rem 3rem;
  font-size: 0.9rem;
  font-weight: 700;
  letter-spacing: 0.1rem;
}
.navbar-brand span{
    color: white;
}
.bg-black {
  background-color: #000 !important;
}

.rounded-pill {
  border-radius: 5rem;
}

.navbar-custom {
  padding-top: 1rem;
  padding-bottom: 1rem;
  background-color: rgba(0, 0, 0, 0.7);
}

.navbar-custom .navbar-brand {
  text-transform: uppercase;
  font-size: 1rem;
  letter-spacing: 0.1rem;
  font-weight: 700;
}

.navbar-custom .navbar-nav .nav-item .nav-link {
  text-transform: uppercase;
  font-size: 0.8rem;
  font-weight: 700;
  letter-spacing: 0.1rem;
}

header.masthead {
  position: relative;
  overflow: hidden;
  padding-top: calc(7rem + 72px);
  padding-bottom: 7rem;
  background-image: url("../assets/food.jpg");
 
  background-repeat: no-repeat;
  background-position: center center;
  background-attachment: scroll;
  background-size: cover;
}
header.masthead .masthead-content {
  z-index: 1;
  position: relative;
}

header.masthead .masthead-content .masthead-heading {
  font-size: 4rem;
}

header.masthead .masthead-content .masthead-subheading {
  font-size: 3rem;
}

header.masthead .bg-circle {
  z-index: 0;
  position: absolute;
  border-radius: 100%;
  background: -webkit-gradient(linear, left bottom, left top, from(#ee0979), to(#ff6a00));
  background: linear-gradient(0deg, #ee0979 0%, #ff6a00 100%);
}

header.masthead .bg-circle-1 {
  height: 90rem;
  width: 90rem;
  bottom: -55rem;
  left: -55rem;
}

header.masthead .bg-circle-2 {
  height: 50rem;
  width: 50rem;
  top: -25rem;
  right: -25rem;
}

header.masthead .bg-circle-3 {
  height: 20rem;
  width: 20rem;
  bottom: -10rem;
  right: 5%;
}

header.masthead .bg-circle-4 {
  height: 30rem;
  width: 30rem;
  top: -5rem;
  right: 35%;
}

@media (max-width: 800px) {
  header.masthead {
    padding-top: calc(10rem + 55px);
    padding-bottom: 10rem;
  }
  header.masthead .masthead-content .masthead-heading {
    font-size: 6rem;
  }
  header.masthead .masthead-content .masthead-subheading {
    font-size: 4rem;
  }
}

.bg-primary {
  background-color: #ee0979 !important;
}

.btn-primary {
  background-color: #ee0979;
  border-color: #ee0979;
}

.btn-primary:active, .btn-primary:focus, .btn-primary:hover {
  background-color: #bd0760 !important;
  border-color: #bd0760 !important;
}

.btn-primary:focus {
  -webkit-box-shadow: 0 0 0 0.2rem rgba(238, 9, 121, 0.5);
  box-shadow: 0 0 0 0.2rem rgba(238, 9, 121, 0.5);
}

.btn-secondary {
  background-color: #ff6a00;
  border-color: #ff6a00;
}

.btn-secondary:active, .btn-secondary:focus, .btn-secondary:hover {
  background-color: #cc5500 !important;
  border-color: #cc5500 !important;
}

.btn-secondary:focus {
  -webkit-box-shadow: 0 0 0 0.2rem rgba(255, 106, 0, 0.5);
  box-shadow: 0 0 0 0.2rem rgba(255, 106, 0, 0.5);
}

</style>
