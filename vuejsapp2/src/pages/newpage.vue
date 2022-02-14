<template>
    <div class="borderg">
        <div class="borderg2">
            <div class="enabutton" id="appe" v-for="item in drinklist" :key="item.id">
                <button @click="buydrink(item.cost, item.id)" :disabled="item.cost>deposite" class="brd">
                    <img class="imagestyle" width="128" height="128" :src="`data:image;base64,${item.image}`" />
                    <b align="center">{{item.name}}</b>
                    <div align="center">{{item.cost}}</div>
                </button>
            </div>
        </div>


        <div class="panel2">

            <p class="borderg2">{{deposite}}</p>

            <div align="center">
                <button class="btncoins" @click="clickbutton(1)">1</button>
                <button class="btncoins" @click="clickbutton(2)">2</button>
            </div>

            <div align="center">
                <button class="btncoins" @click="clickbutton(5)">5</button>
                <button class="btncoins" @click="clickbutton(10)">10</button>
            </div>

            <div align="center" class="btncashback" margin="40px ">

                <button @click="sdacha()">Сдача</button>
            </div>

        </div>


    </div>

</template>

<script>
    import axios from "axios";
    export default {
        data() {
            return {
                drinklist: [],
                coinslist: [],
                coins: [],
                depositecoins: [0,0,0,0],
                cashback: [0,0,0,0],
                deposite: 0,
                _id: 0,
            }
        },
        methods: {
            clickbutton(naminal) {
                this.deposite += naminal;
                this.coins.push(Number(naminal));
                console.log(this.coins);
            },

            async sdacha() {
                this.cashback = [0, 0, 0, 0];

                var response = await axios.get(`https://localhost:5001/api/GetCoinList/${this._id.id}`);
                this.coinslist = response.data;
                console.log(this.coinslist);

                while (this.deposite >= 1 && this.coinslist[0]!=0) {
                    if (this.deposite >= 10 && this.coinslist[3]>=1) {
                        this.deposite -= 10;
                        this.cashback[3]++;
                    }
                    else {
                        if (this.deposite >= 5 && this.coinslist[2] >= 1) {
                            this.deposite -= 5;
                            this.cashback[2]++;
                        }
                        else
                        {
                            if (this.deposite >= 2 && this.coinslist[1] >= 1) {
                                this.deposite -= 2;
                                this.cashback[1]++;
                            }
                            else
                            {
                                if (this.deposite >= 1 && this.coinslist[0] >= 1) {
                                    this.deposite -= 1;
                                    this.cashback[0]++;
                                }
                            }
                           
                        }
                        
                    }
                    
                }

                for (var i = 0; i < 4; i++) {
                    this.cashback[i] = this.cashback[i] * (-1);
                }

                alert(`1 руб -${this.cashback[0]}, 2 руб -${this.cashback[1]}, 5 руб -${this.cashback[2]}, 10 руб -${this.cashback[3]}.`);

                await axios.put(`https://localhost:5001/api/GetCoinList/${this._id.id}`, this.cashback);
            },

            async buydrink(cost, iddrink){
                
                this.depositecoins[0] += this.coins.filter(x=>x == 1).length;
                this.depositecoins[1] += this.coins.filter(x=>x == 2).length;
                this.depositecoins[2] += this.coins.filter(x=>x == 5).length;
                this.depositecoins[3] += this.coins.filter(x=>x == 10).length;

                this.coins = [];
                console.log(this.depositecoins); 
                this.deposite = this.deposite- cost;

                await axios.put(`https://localhost:5001/api/GetCoinList/${this._id.id}`, this.depositecoins);
                this.depositecoins = [0,0,0,0];
                await axios.put(`https://localhost:5001/api/GetDrinkList/${this._id.id}?drinkid=${iddrink}`, iddrink);

                this.oncreate();
            },

            async oncreate() {
                try {
                    this.drinklist = [];
                    var response = await axios.get(`https://localhost:5001/api/GetDrinkList/${this._id.id}`);
                    this.drinklist = response.data;

                    console.log(this.drinklist);

                    response = await axios.get(`https://localhost:5001/api/GetCoinList/${this._id.id}`);
                    this.coinslist = response.data;
                    console.log(this.coinslist);
                }
                catch (e) {
                    alert(e.data)
                }
            }
        },
        mounted() {
            this._id = this.$route.params;

            this.oncreate();
        }

    }
</script>

<style>

    .nw {
        justify-content: center;
    }

    .enabutton {
        border: 0px;
    }

    .borderg2 {
        background: #d1dbde;
        border-radius: 15px;
        padding: 10px;
        display: flex;
        justify-content: center;
        flex-wrap: wrap;
    }

    .lil {
        height: 100%;
    }

    .btncoins {
        width: 10%;
        margin: 2px;
        border: 2px solid gray;
        background-color: lightgray;
        height: 40px;
    }

    .panel2 {
        background: #a39f93;
        border-radius: 15px;
        justify-content: center;
        border: 4px solid #006400;
        margin: 0px 40px 10px 40px;
        padding: 10px;
        width: 100%;
        display: initial;
        height: 100%;
        max-width: 600px;
    }

    .panel3 {
        justify-content: center;
        padding: 10px;
        width: 100%;
        display: initial;
        height: 100%;
        max-width: 600px;
    }

    .borderg {
        background: #d1dbde;
        margin: 30px;
        border-radius: 15px;
        border: 4px solid #006400;
        padding: 10px;
        width: 100%;
        height: 100%;
        min-height: 300px;
        min-width: 180px;
        display: flex;
        justify-content: center;
    }

    .imagestyle {
        width: 90px;
        height: 90px;
        margin: 0px 0px 0px -12px;
    }

    .brd {
        border: 2px solid darkgrey; /* ��������� ������� */
        padding: 16px; /* ���� ������ ������ */
        width: 100px;
        height: 160px;
        font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
        border-radius: 8px;
        flex-wrap: wrap;
        align-content: center;
    }

    .post {
        padding: 10px;
        border: 2px solid teal;
        margin: 10px;
        flex-wrap: wrap;
        align-content: center;
    }

    .btn {
        margin: 10px, 5px,10px,5px;
        background: none;
        color: teal;
        border: 2px solid teal;
    }

    .test2 {
        margin: 0 25% 0 25%;
        width: 50%;
    }

    .btncashback {
        margin: 15px 20px;
        background: none;
        color: teal;
        border: 2px;
    }
</style>