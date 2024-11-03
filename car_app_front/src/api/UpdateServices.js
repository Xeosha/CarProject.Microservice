import axios from 'axios';

export const UpdateService = async (serviceOrgId, orgId, serviceId, Price, Description) => {
  try {
    const response = await axios
    .post("https://localhost:6061/api/Organization/updateService", {
        serviceOrgId,
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
