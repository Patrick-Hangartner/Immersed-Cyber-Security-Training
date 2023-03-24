import { lazy } from "react";

const PhishingEmail = lazy(() => 
import("../components/authentication/PhishingEmail")
);

const routes = [
{
    path: "/phishing",
    name: "Phishing Email",
    exact: false,
    element: PhishingEmail,
    roles: [],
    isAnonymous: true,
  },

  {
    path: "/trainee/confirm",
    name: "Trainee Confirmated",
    exact: false,
    element: PhishingEmail,
    roles: [],
    isAnonymous: true,
  },
]; 
var allRoutes = [
  ...routes,
  ...errorRoutes,
  ...sharedStoriesRoute,
  ...addStoryRoute,
];
    
 export default allRoutes;
