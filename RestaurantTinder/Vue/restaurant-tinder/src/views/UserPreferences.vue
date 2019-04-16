<template>
    <div id="preferences">
        <div class="form-preferences " id="current-preferences">
            <h1 class="mb-3 " >Your Current Preferences</h1>
            <ul class="list-group" >
                <li class="list-group-item" v-for="pref in currentPrefsList" :key="pref.id">{{ pref.name }}</li>
            </ul>
        </div>
        <form class="form-preferences" @submit.prevent="SavePrefs">
            <h1 class="mb-3">Add a food preference here</h1>
            <div class="alert alert-danger" role="alert" v-if="registrationErrors">
                There were problems registering this user.
            </div>
            <label for="preference" class="sr-only">Preference</label>
            <input
                type="text"
                id="preference"
                class="form-control"
                placeholder="e.g. Mexican"
                v-model="preference"
                autofocus
            />
            <button class="btn btn-lg btn-primary btn-block" type="submit">
                Add Preference
            </button>
        </form>
    </div>
</template>

<script>
import auth from '../auth';
export default {
name: 'UserPreferences',
  components: {
  },
  data() {
    return {
        preference:'',
        currentPrefs: [],
        registrationErrors: false,
    }
  },
    computed:{
        currentPrefsList(){
            let prefs = [];
                    for(let i = 0; i < this.currentPrefs.length; i++){                
                    prefs.push({
                    id: i,
                    name: this.currentPrefs[i].name,       
                });
            }
            return prefs;
        }
    },
    methods: {

        SavePrefs() {

          let ajaxURL = `${process.env.VUE_APP_REMOTE_API}` + "/api/savepreference";

          //http://localhost:50260/api/savepreference
          fetch(ajaxURL, {
              method: 'post',
              headers: {
                  "Content-Type": "application/json",
                  Authorization : 'Bearer ' + auth.getToken(),
              },
              credentials: 'same-origin',
              body: JSON.stringify(this.preference)
          })
          .then((response) => {
              return response.text();
          })
          .then((data) => {
              window.console.log(data);
              this.GetCurrentPrefs();       
          })
          .catch((error) => {
              window.console.log('Error:', error);
          });
        },
        UpdatePrefs(){
            this.prefsToAdd.push({name: this.preference});
        },
        GetCurrentPrefs() {
          let ajaxURL = `${process.env.VUE_APP_REMOTE_API}` + "/api/preferences";

          //http://localhost:50260/api/preferences
          fetch(ajaxURL, {
              method: 'get',
              headers: {
                  "Content-Type": "application/json",
                  Authorization : 'Bearer ' + auth.getToken(),
              },
              credentials: 'same-origin',
          })
          .then((response) => {
              return response.json();
          })
          .then((data) => {
              window.console.log(data); 
              this.currentPrefs = data;      
          })
          .catch((error) => {
              window.console.log('Error:', error);
          });
        }
    },
    created() {
      this.GetCurrentPrefs();
    }
}
</script>

<style>
html,
body {
  height: 100%;
}
#app {
  height: 100%;
}
h1 {
  font-size: 18px!important;
}
li{
  font-size: 14px!important;
  padding: .25rem .75rem!important;
}
button{
  padding: 0rem!important;
  font-size: 16px!important;
  background-color: #ee0979!important;
  border-color: #ee0979!important;
}
.preferences {
  height: 100%;
  display: -ms-flexbox;
  display: flex;
  -ms-flex-align: center;
  align-items: center;
  padding-top: 20px;
  padding-bottom: 20px;
  background-color: #f5f5f5;
  font-family: 'Roboto Condensed', sans-serif;
}

form {
  text-align: center;
}

.form-preferences {
  width: 100%;
  max-width: 330px;
  padding: 15px;
  margin: auto;
}

.form-preferences .form-control {
  position: relative;
  box-sizing: border-box;
  height: auto;
  padding: 10px;
  font-size: 14px;
}
.form-preferences .form-control:focus {
  z-index: 2;
}
.form-preferences input[type='text'] {
  margin-bottom: 5px;
  border-bottom-right-radius: 0;
  border-bottom-left-radius: 0;
}
#preferences ul{
  margin-bottom: 5px;
  border-bottom-right-radius: 0;
  border-bottom-left-radius: 0;
}
#current-preferences{
    text-align: center;
}
/* .form-preferences input[type='password'] {
  margin-bottom: 10px;
  border-top-left-radius: 0;
  border-top-right-radius: 0;
} */
</style>
