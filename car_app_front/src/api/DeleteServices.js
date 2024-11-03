import axios from 'axios';

const DeleteService = async (serviceOrgId) => {
  try {
    const response = await axios
    .post("https://localhost:6061/api/Organization/deleteService", {
        serviceOrgId
    });
    return response.data;
  } catch (error) {
      console.error('Login error:', error.response ? error.response.data : error.message);
      throw error;
  }
};

export default DeleteService;