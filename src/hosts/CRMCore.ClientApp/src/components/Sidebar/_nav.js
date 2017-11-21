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
      name: 'CRM',
      icon: 'icon-credit-card',
      children: [
        {
          name: 'Contacts',
          url: '/crm/contacts',
          icon: 'icon-people',
          badge: {
            variant: 'info'
          }
        },
        {
          name: 'Customers',
          url: '/crm/customers',
          icon: 'icon-diamond',
          badge: {
            variant: 'info'
          }
        },
        {
          name: 'Lead Sources',
          url: '/crm/lead-sources',
          icon: 'icon-user',
          badge: {
            variant: 'info'
          }
        },
        {
          name: 'Pipeline',
          url: '/crm/pipeline',
          icon: 'icon-directions',
          badge: {
            variant: 'info',
            text: 'NEW'
          }
        }
      ]
    },
    {
      name: 'Metadata',
      icon: 'icon-layers',
      children: [
        {
          name: 'Fields',
          url: '/metadata/fields',
          icon: 'icon-doc',
          badge: {
            variant: 'info'
          }
        },
        {
          name: 'Schemas',
          url: '/metadata/schemas',
          icon: 'icon-docs',
          badge: {
            variant: 'info'
          }
        }
      ]
    },
    {
      name: 'Pages',
      url: '/pages',
      icon: 'icon-star',
      children: [
        {
          name: 'Docs',
          url: '/docs/',
          icon: 'icon-star'
        },
        {
          name: 'Swagger',
          url: '/swagger',
          icon: 'icon-star'
        },
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
