<template>
  <div id="app">
    <div class="container-flex">
      <h1 class="hello">nas uri shortener</h1>
      <label>URI</label>
      <input class="" type="text" v-model="input.link"/>
      <br/>
      <br/>
      <label>Slug</label>
      <input class="" type="text" v-model="input.slug"/>
      <br/>
      <br/>
      <input type="button" v-on:click="create()" value="Shorten">
    </div>
    </div>
</template>

<script>
import axios from "axios";

export default {
  name: 'App',
  data() {
    return{
      input:{
      link: "",
      slug:""
      }
    }
  },
  methods: {
    create() {
      try {
        // TODO: add the port.
        await axios.post(`https://localhost:${port}`,{
        body:this.input
      })
      } catch (err) {
        console.log(err)
      }
      console.log(this.input.link)
      console.log(this.input.slug)
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
  
}
div{
}
div.container-flex{
  border: 1px solid #ccc!important;
  padding: 20px;
  border-radius: 16px;
  width:800px; margin:0 auto;
}
</style>
