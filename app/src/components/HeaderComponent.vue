<template>
  <header>
    <div class="container-logo-input-search">
      <img class="twitter-logo" src="/images/twitter-logo.png" alt="" />
      <div class="search-box">
        <input type="text" class="search-txt" placeholder="Search" />
        <RouterLink to="/" class="search-btn">
          <img class="lupa" src="/images/lupa.png" alt="lupa" />
        </RouterLink>
      </div>
    </div>

    <nav>
      <router-link to="/">
        <div class="container-navbar">
          <img class="navbar-icon" src="/images/home.png" alt="" />
          <p class="navbar-text">Home</p>
        </div>
      </router-link>
      <router-link to="/notification">
          <div class="container-navbar-notification">
            <div class="navbar-notification-count">5</div>
            <img src="/images/notificacoes.png" class="navbar-icon" alt="">
          </div>
      </router-link>


      <div class="navbar-perfil-data">
        
      </div>
    </nav>
  </header>
</template>

<script>
export default {
  name: "header",
};
</script>

<style scoped>
@import url("https://fonts.googleapis.com/css2?family=ADLaM+Display&display=swap");
/* Header */
header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem;
}
.container-logo-input-search {
  display: flex;
  justify-content: center;
  align-items: center;
}
/* ---------------- */
/* globais */
a {
  text-decoration: none;
}
/* routerlink */
.router-link-active .container-navbar {
  width: 105px;;
  background-color: rgb(0, 145, 255);
  color: white;
}
.router-link-active .container-navbar .navbar-icon {
  filter: brightness(70%);
}
/* -------------------- */
/* navbar */
nav {
  display: flex;
  gap: 20px;
  justify-content: center;
  align-items: center;
}
.container-navbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-family: "ADLaM Display", cursive;
  color: black;
  background-color: white;
  padding: 0.5rem;
  border-radius: 20px;
  overflow: hidden;
  gap: 5px;
  width: 40px;
  transition: 0.2s ease-in-out;
  cursor: pointer;
  padding-right: 1rem;
}
.container-navbar-notification{
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-family: "ADLaM Display", cursive;
  color: black;
  background-color: white;
  padding: 0.5rem;
  border-radius: 20px;
  
  gap: 5px;
  width: 40px;
  transition: 0.2s ease-in-out;
  cursor: pointer;
  padding-right: 1rem;
  position: relative;
}
.navbar-icon {
  width: 26px;
}
.container-navbar:hover {
  width: 105px;
  background-color: rgb(0, 145, 255);
  color: white;
}
.container-navbar:hover > .navbar-icon{
  filter: brightness(70%);
}
.navbar-notification-count{
  background-color: rgb(0, 166, 255);
  height: 20px;
  width: 20px;
  position: absolute;
  top: -10px;
  right: 0;
  text-align: center;
  border-radius: 50%;
  color: white;
}
/* ------------------ */
/* input search */
.search-box {
  background-color: rgb(11, 54, 145);
  height: 42px;
  top: 10px;
  left: 80px;
  padding: 0.625rem;
  border-radius: 40px;
  margin-left: 1rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.search-txt {
  border: none;
  background-color: transparent;
  outline: none;
  color: white;
  font-size: 1em;
  width: 0;
  transition: 0.2s ease-in-out;
  font-family: "ADLaM Display", cursive;
}
.search-txt::placeholder {
  color: rgba(255, 255, 255, 0.245);
}
.lupa {
  background-color: transparent;
  width: 23px;
}
.search-btn {
  background-color: transparent;
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  transition: 0.2s ease-in-out;
}
.search-box:hover > .search-txt {
  width: 200px;
}
.search-box:hover > .search-btn {
  background-color: rgb(2, 17, 50);
}
/* ------------------------------ */
.twitter-logo {
  width: 50px;
}
</style>
