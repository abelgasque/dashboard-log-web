export const environment = {
  nuVersion: "v1.1.2102.3",
  production: false,    
  useUrlProd: true,  
  baseUrlProd: "https://localhost:44354",
  baseUrlDev: "https://localhost:5001",
  tokenWhitelistedDomains: [
    new RegExp('localhost:5001'), 
    new RegExp('localhost:44354'),
  ],
  tokenBlacklistedRoutes: [ 
    new RegExp('\/ws\/Token\/Authenticate'),
  ]
};
