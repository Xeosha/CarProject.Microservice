import axios from 'axios';

export const ShowService = async (orgId) => {
  try {
    const response = await axios
    .post("https://localhost:6061/api/Organization/showService", {
        orgId
    });
    return response.data;
  } catch (error) {
      console.error('Login error:', error.response ? error.response.data : error.message);
      throw error;
  }
};
