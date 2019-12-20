import Axios from "axios";

export default Axios.create({
  baseURL: 'http://localhost:56931/api/'
});