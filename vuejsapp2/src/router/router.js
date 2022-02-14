import mainApp from "@/pages/mainApp";
import newpage from "@/pages/newpage";
import { createRouter, createWebHashHistory } from "vue-router";

const routes = [
    {
        path: '/',
        component: mainApp,
    },
    {
        path: '/main2/:id',
        component: newpage,
    },

]

const router = createRouter({
    routes,
    history: createWebHashHistory(process.env.BASE_URL)
})

export default router;