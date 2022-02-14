<template>
<div class="bar">

<div v-for="item in arraya" :key="item.id">
     <div class="bar">
       <div @click="$router.push(`/main2/${item.id}`)">
        <img class="imageclass" src="../res/vm.png"/>
        <div>Машина: {{item.id}}</div>

       </div>
      </div>
</div>
</div>
</template>

<script>
    import axios from "axios";
export default {
   data() {
            return {
              arraya:[]
            }
   },
   methods: {

            logclick() {
                
            },

            async oncreate() {
                try {
                    const response = await axios.get('https://localhost:5001/api/VendingMachines');
                    console.log(response.data);
                    this.arraya = response.data;

                }
                catch (e) {
                    alert(e)
                }
            }
        },
        mounted() {
            this.oncreate();
        }

}
</script>

<style>

.bar{
  display: flex;
  margin: 40px;
  justify-content:center;
}
.imageclass{
  width: 100px;
  height: 150px;
}
</style>