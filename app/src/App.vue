<template>
  <HeaderComponent></HeaderComponent>
  <router-view></router-view>
</template>
<script>
  import HeaderComponent from './components/HeaderComponent.vue';
  export default{
    components:{HeaderComponent}
  }
</script>
<style>
  *{
    margin: 0rem;
    padding: 0rem;
    box-sizing: border-box;
  }
  body{
    background-color: rgb(2, 17, 50);
  }
</style>

