export const environment = {
  nuVersion: "1.0.0",
  production: false,
  baseUrl: "https://localhost:5001",
  tokenWhitelistedDomains: [
    new RegExp('localhost:5001'),
  ],
  tokenBlacklistedRoutes: [ 
    new RegExp('\/ws\/Token\/Authenticate'),
  ]
};
