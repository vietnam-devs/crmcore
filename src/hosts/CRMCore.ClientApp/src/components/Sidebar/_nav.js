export default {
  items: [
    {
      name: 'Dashboard',
      url: '/dashboard',
      icon: 'icon-speedometer',
      badge: {
        variant: 'info'
      }
    },
    {
      name: 'Contacts',
      url: '/contacts',
      icon: 'icon-people',
      badge: {
        variant: 'info',
        text: 'NEW'
      }
    },
    {
      name: 'Customers',
      url: '/customers',
      icon: 'icon-diamond',
      badge: {
        variant: 'info',
        text: 'NEW'
      }
    },
    {
      name: 'Lead Sources',
      url: '/lead-sources',
      icon: 'icon-user',
      badge: {
        variant: 'info',
        text: 'NEW'
      }
    },
    {
      name: 'Pipeline',
      url: '/pipeline',
      icon: 'icon-directions',
      badge: {
        variant: 'info',
        text: 'NEW'
      }
    },
    {
      name: 'Pages',
      url: '/pages',
      icon: 'icon-star',
      children: [
        {
          name: 'Login',
          url: '/login',
          icon: 'icon-star'
        },
        {
          name: 'Register',
          url: '/register',
          icon: 'icon-star'
        },
        {
          name: 'Error 404',
          url: '/404',
          icon: 'icon-star'
        },
        {
          name: 'Error 500',
          url: '/500',
          icon: 'icon-star'
        }
      ]
    }
  ]
};
