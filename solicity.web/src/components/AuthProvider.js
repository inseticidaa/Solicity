import { createContext, useContext, useMemo } from "react";
import { useNavigate } from "react-router-dom";
import { useLocalStorage } from "./useLocalStorage";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {

  const [user, setUser] = useLocalStorage("user", null);
  const [token, setToken] = useLocalStorage("token", null);
  const navigate = useNavigate();

  const login = async (email, password, callback) => {

    let api_url = new URL(process.env.REACT_APP_API_URL);
    api_url.pathname = "/api/Auth/SignIn";

    let body = { email, password };

    fetch(
      api_url,
      {
        method: 'POST',
        body: JSON.stringify(body),
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
      })
      .then((response, err) => {

        if (err) {
          return callback(err)
        }

        response.json()
          .then(res => {
            switch (response.status) {
              case 400:
              case 401:
                callback("Unauthorized Sign In")
                break;
              case 200:
                setUser(res.user);
                setToken(res.token);
                callback()
                break;
            };         
          });
      })
  };

  const logout = () => {
    setUser(null);
    setToken(null);
    navigate("/login", { replace: true });
  };

  const value = useMemo(
    () => ({
      user,
      token,
      login,
      logout
    }),
    [user]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  return useContext(AuthContext);
};
