import axios from 'axios';

const CreateService = async (orgId, serviceId, Price, Description) => {
  try {
    const response = await axios
    .post("https://localhost:6061/api/Organization/createService", {
        orgId,
        serviceId,
        Price,
        Description
    });
    return response.data;
  } catch (error) {
      console.error('Login error:', error.response ? error.response.data : error.message);
      throw error;
  }
};

export default CreateService;