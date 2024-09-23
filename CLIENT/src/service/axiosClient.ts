import axios, { AxiosRequestConfig } from 'axios';

class ApiService {
  private instance = axios.create({
    baseURL: 'https://localhost:7059/api', // Cambia la URL base según tu caso
    timeout: 5000000,
    headers: {
      'Content-Type': 'application/json',
    },
  });

  // Método genérico para hacer GET requests
  async get<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
    const response = await this.instance.get<T>(url, config);
    return response.data;
  }

  // Método genérico para hacer POST requests
  async post<T, U>(url: string, data: U, config?: AxiosRequestConfig): Promise<T> {
    const response = await this.instance.post<T>(url, data, config);
    return response.data;
  }

  // Método genérico para hacer PUT requests
  async put<T, U>(url: string, data: U, config?: AxiosRequestConfig): Promise<T> {
    const response = await this.instance.put<T>(url, data, config);
    return response.data;
  }

  // Método genérico para hacer DELETE requests
  async delete<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
    const response = await this.instance.delete<T>(url, config);
    return response.data;
  }
}

const apiService = new ApiService();
export default apiService;
