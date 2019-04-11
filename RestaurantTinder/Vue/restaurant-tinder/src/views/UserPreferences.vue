<template>
    <div id="preferences">
        <div class="form-preferences " id="current-preferences">
            <h1 class="h3 mb-3 font-weight-normal" >Your Current Preferences</h1>
            <ul class="list-group" >
                <li class="list-group-item" v-for="pref in currentPrefsList" :key="pref.id">{{ pref.name }}</li>
            </ul>
        </div>
        <form class="form-preferences" @submit.prevent="UpdatePrefs">
            <h1 class="h3 mb-3 font-weight-normal">Add a food preference here</h1>
            <div class="alert alert-danger" role="alert" v-if="registrationErrors">
                There were problems registering this user.
            </div>
            <label for="preference" class="sr-only">Preference</label>
            <input
                type="text"
                id="preference"
                class="form-control"
                placeholder="Mexican"
                v-model="preference"
                autofocus
            />
            <button class="btn btn-lg btn-primary btn-block" type="submit">
                Add Preference
            </button>
        </form>
        <form class="form-preferences" @submit.prevent="SavePrefs">
            <h1 class="h3 mb-3 font-weight-normal">Food Preferences</h1>
            <div class="alert alert-danger" role="alert" v-if="registrationErrors">
                There were problems registering this user.
            </div>
            <ul class="list-group">
                <li class="list-group-item" v-for="pref in prefsToAddList" :key="pref.id">{{ pref.name }}</li>
            </ul>
            <button class="btn btn-lg btn-primary btn-block" type="submit">
                Save Preferences
            </button>
        </form>

    </div>
</template>

<script>

export default {
name: 'UserPreferences',
  components: {
  },
  data() {
    return {
        preference:'',
        prefsToAdd: [],
        currentPrefs: [{name:'Thai'}],
        registrationErrors: false,
    }
  },
    computed:{
        prefsToAddList(){
            let prefs = [];
                    for(let i = 0; i < this.prefsToAdd.length; i++){                
                    prefs.push({
                    id: i,
                    name: this.prefsToAdd[i].name,       
                });
            }
            return prefs;
        },
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
        const player = {};
        player.name = this.addPlayerName;

        let ajaxURL = `${process.env.VUE_APP_REMOTE_API}` + "api/player";

        //http://localhost:50260/api/player
        fetch(ajaxURL, {
            method: 'post',
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(player)
        })
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            window.console.log(data);
            this.updateRemainingDice(data.turnStatus.remainingDice);
            this.getPlayers();         
        })
        .catch((error) => {
            window.console.log('Error:', error);
        });
        },
        UpdatePrefs(){
            this.prefsToAdd.push({name: this.preference});
        }
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
.preferences {
  height: 100%;
  display: -ms-flexbox;
  display: flex;
  -ms-flex-align: center;
  align-items: center;
  padding-top: 40px;
  padding-bottom: 40px;
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
  font-size: 16px;
}
.form-preferences .form-control:focus {
  z-index: 2;
}
.form-preferences input[type='text'] {
  margin-bottom: 10px;
  border-bottom-right-radius: 0;
  border-bottom-left-radius: 0;
}
#preferences ul{
  margin-bottom: 10px;
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
